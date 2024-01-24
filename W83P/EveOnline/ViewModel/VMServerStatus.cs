using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using W83P.Basic;
using W83P.EveOnline.Model;

namespace W83P.EveOnline.ViewModel
{
    public class VMServerStatus : ViewModelBase<MServerStatus>
    {

        public new async Task AddJsonObjectToListAsync(string jsonObject)
        {
            try
            {
                JObject obj = JObject.Parse(jsonObject);
                await OnJsonObjectToAdd(obj);
                #pragma warning disable CS8604 // Possible null reference argument.
                Items.Add(obj.ToObject<MServerStatus>());
                #pragma warning restore CS8604 // Possible null reference argument.
                OnPropertyChanged("Items");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding json object to list: " + ex.Message);
            }
        }

        public void SaveToFile(string file){
            byte[] binaryData = BinarySerializer<ObservableCollection<MServerStatus>>.Serialize(Items);

            using (var fileStream = new FileStream(file, FileMode.Create)){
                fileStream.Write(binaryData, 0, binaryData.Length);
            }
        }

        public void LoadFromFile(string file){
            using (var fileStream = new FileStream(file, FileMode.Open)){
                var binaryData = new byte[fileStream.Length];
                fileStream.Read(binaryData, 0, binaryData.Length);
                Items = BinarySerializer<ObservableCollection<MServerStatus>>.Deserialize(binaryData);
            }
        }
    }
}