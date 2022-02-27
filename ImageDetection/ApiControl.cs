using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImageDetection
{
    public class Model
    {
        public Requests[] requests { get; set; }
    }
    public class Requests
    {
        public Features[] features { get; set; }
        public Images image { get; set; }
    }
    public class Images
    {
        public Sources source { get; set; }
    }
   
    public class Features
    {
        public string type { get; set; }
    }

    public class Sources
    {
        public string imageUri { get; set; }
    }

    public class ApiControl
    {
        public ApiModel value { get; set; }
        public Model give { get; set; }
        public ApiControl(string fileName)
        {
            string fname = "gs://osssk/" + fileName;
            var model = new Model();

            var feat = new Features() { type = "TEXT_DETECTION" };
            var req = new Requests() {features=new Features[1], image = new Images() { source = new Sources() { imageUri = fname } } };
            req.features[0] = feat;

            model.requests = new Requests[1];
            model.requests[0] = req;
            give = model;
            Get(give);
        }

        public void Get(Model post)
        {
            // Serialize our concrete class into a JSON String
            var stringPayload = JsonConvert.SerializeObject(post);

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();

            // Do the actual request and await the response
            var httpResponse = httpClient.PostAsync("https://eu-vision.googleapis.com/v1/images:annotate?key=AIzaSyBcax1jrYgIYpEnFsfWv97Btijgl7AIO38", httpContent);
            var responseContent =  httpResponse.Result.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<ApiModel>(responseContent.Result);
            value = user;
           

        } 
    }
}
