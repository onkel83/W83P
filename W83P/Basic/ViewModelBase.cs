#pragma warning disable CS8618, CS8625, CS8604, CS8612
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace W83P.Basic
{
    public class ViewModelBase
    {
        // ...
    }

    public abstract class ViewModelBase<T> : INotifyPropertyChanged
        where T : class
    {
        private ObservableCollection<T> _items;

        protected ViewModelBase()
        {
            _items = new ObservableCollection<T>();
        }

        public ObservableCollection<T> Items
        {
            get { return _items; }
            set => _items = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual Task OnJsonObjectToAdd(JObject jsonObject)
        {
            // ...
            return Task.CompletedTask;
        }

        protected virtual Task OnJsonArrayToAdd(JArray jsonArray)
        {
            // ...
            return Task.CompletedTask;
        }

        public async Task AddJsonObjectToListAsync(string jsonObject)
        {
            try
            {
                JObject obj = JObject.Parse(jsonObject);
                await OnJsonObjectToAdd(obj);
                Items.Add(obj.ToObject<T>());
                OnPropertyChanged("Items");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding json object to list: " + ex.Message);
            }
        }

        public async Task AddJsonArrayToListAsync(string jsonArray)
        {
            try
            {
                JArray array = JArray.Parse(jsonArray);
                foreach (JObject obj in array)
                {
                    await OnJsonObjectToAdd(obj);
                    Items.Add(obj.ToObject<T>());
                }
                OnPropertyChanged("Items");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding json array to list: " + ex.Message);
            }
        }
    } 
}
#pragma warning restore CS8618, CS8625, CS8604, CS8612