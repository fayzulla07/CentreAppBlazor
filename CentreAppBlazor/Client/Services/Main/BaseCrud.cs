using CentreAppBlazor.Client.Extensions;
using CentreAppBlazor.Shared.Domain;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentreAppBlazor.Client.Services.Main
{
    public abstract class BaseCrud<T> : ComponentBase
    {
        protected int count = 0;

        public string ErrorMessage { get; set; }
        protected abstract string ApiUrl { get; set; }
        protected bool IsResponsive = false;
        [Inject]
        public IAppService serv { get; set; }
        [Inject]
        protected LoadingService loading { get; set; }
        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        private NotificationService notificationService{ get; set; }

        protected RadzenDataGrid<T> Grid { get; set; }
        public T temp;
        protected T dataform;
        protected List<T> dsource = new List<T>();
        protected override void OnInitialized()
        {
            dataform = ReflectionExten.GetNewObject<T>();
            
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    loading.Visible = true;
                    await GetData();
                    await GetForDropDown();
                    await InvokeAsync(StateHasChanged);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await ShowNotification(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Ошибка", Detail = "Ошибка при получении данных", Duration = 2500 });
                }
                finally
                {
                    loading.Visible = false;
                }
            }
        }

        protected async virtual Task GetData()
        {
            dsource = new List<T>(await serv.GetAsync<List<T>>(ApiUrl));
            count = dsource.Count;
        }
        protected async virtual Task UpdateData(T item)
        {
            int Id = (int)ReflectionExten.GetPropValue(item, "Id");
            await serv.UpdateAsync<T>(item, Id, ApiUrl);
        }
        protected async virtual Task DeleteData(int Id)
        {
            await serv.DeleteAsync(Id, ApiUrl);
        }
        protected void EditRow(T item)
        {
            temp = CloneExtension.Clone<T>(item);
            Grid.EditRow(item);
        }

        protected async void OnUpdateRow(T item)
        {
            try
            {
                loading.Visible = true;
                await UpdateData(item);

               // int Id = (int)ReflectionExten.GetPropValue(item, "Id");
               // var value = dsource.FindIndex(x => (int)ReflectionExten.GetPropValue(x, "Id") == Id);
               //  dsource[value] = result;

                await ShowNotification(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Успешно", Detail = "Успешно Изменено", Duration = 1500 });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await ShowNotification(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Ошибка", Detail = "Ошибка при получении", Duration = 2500 });
            }
            finally
            {
                loading.Visible = false;
            }
        }

        protected void SaveRow(T item)
        {
            Grid.UpdateRow(item);
        }

        protected void CancelEdit(T item)
        {
            Grid.CancelEditRow(item);
            int Id = (int)ReflectionExten.GetPropValue(item, "Id");
            var value = dsource.FindIndex(x => (int)ReflectionExten.GetPropValue(x, "Id") == Id);
            dsource[value] = temp;
            StateHasChanged();
        }

        protected async void DeleteRow(T item)
        {
            try
            {
                loading.Visible = true;
                int Id = (int)ReflectionExten.GetPropValue(item, "Id");
                await DeleteData(Id);
                dsource.Remove(item);
                count = dsource.Count;
                await ShowNotification(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Успешно", Detail = "Успешно Удалено", Duration = 1500 });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await ShowNotification(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Ошибка", Detail = string.IsNullOrEmpty(ErrorMessage) ? "Ошибка при получении" : ErrorMessage, Duration = 2500 });
            }
            finally
            {
                loading.Visible = false;
                await Grid.Reload();
            }
        }
        protected virtual async void OnSubmit(T model)
        {
            try
            {
                loading.Visible = true;
                var result = await serv.InsertAsync<T>(model, ApiUrl);

                dsource.Insert(0, result);
                dataform = ReflectionExten.GetNewObject<T>();
                await ShowNotification(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Успешно", Detail = "Успешно Добавлено", Duration = 1500 });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await ShowNotification(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Ошибка", Detail = "Ошибка при получении", Duration = 2500 });

            }
            finally
            {
                loading.Visible = false;
                await Grid.Reload();
            }
        }

        protected void OnInvalidSubmit(FormInvalidSubmitEventArgs args)
        {

        }
        protected async Task ShowNotification(NotificationMessage message)
        {
            notificationService.Notify(message);
            await InvokeAsync(StateHasChanged);
        }
        protected void OkDialog(T item)
        {
            DeleteRow(item);
            DialogService.Close(true);
        }
        protected void CancelDialog()
        {
            DialogService.Close(false);
        }

        protected virtual Task GetForDropDown()
        {
           return Task.CompletedTask;
        }
    }
}
