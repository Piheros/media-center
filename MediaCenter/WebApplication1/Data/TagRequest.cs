using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MediaCenter.Models;

namespace MediaCenter.Controllers
{
    public class TagRequest
    {
        // Second Key = ce55c5a06c2e43c893e56cb387ff77ca
        const string subscriptionKey = "0dae4fb1c3c84b7782db8330cb1e2db0";

        // https://[location].api.cognitive.microsoft.com/vision/v1.0/analyze[?visualFeatures][&details][&language] 
        // ERROR 401 : {"error":{"code":"Unspecified","message":"Access denied due to invalid subscription key. 
        // Make sure you are subscribed to an API you are trying to call and provide the right key."}}
        const string uriBase = "https://francecentral.api.cognitive.microsoft.com/vision/v1.0/tag";
        
        // Request to Azure Cognitive services
        internal static async Task MakeAnalysisRequest(Image image)
        {
            byte[] byteData = image.Data;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            string requestParameters = "visualFeatures=Categories,Description,Color&language=en";
            string uri = uriBase + "?" + requestParameters;

            HttpResponseMessage response;

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // Content type "application/octet-stream"
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
                string contentString = await response.Content.ReadAsStringAsync();

                // Display the JSON response
                Console.WriteLine("/nResponse:/n");
                Console.WriteLine(JsonPrettyPrint(contentString));
                List<String> tags = new List<String>();
                GetTags(image, contentString);
            }
        }

        // Get name and confidence of Tag
        internal static void GetTags(Image image, string contentString)
        {
            List<Tag> listTags = new List<Tag>();
            JObject jOb = JObject.Parse(contentString);
            var tags = jOb["tags"];

            foreach (var tag in tags)
            {
                Tag tagImage = new Tag();
                tagImage.ImageId = image.Id;

                // Name of tags
                tagImage.Nom = tag["name"].ToString();

                // Percentage of similarity
                float size = float.Parse(tag["confidence"].ToString());
                tagImage.Size = (int)(size * 100);
                image.ListTag.Add(tagImage);
            }
        }

        // Token for connection
        private static IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;
                foreach (var cc in AllChildren(c))
                {
                    yield return cc;
                }
            }
        }

        // Image with bytes
        internal static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

        // Parse JSON
        static string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            int offset = 0;
            int indentLength = 3;

            foreach (char ch in json)
            {
                switch (ch)
                {
                    case '"':
                        if (!ignore) quote = !quote;
                        break;
                    case '\'':
                        if (quote) ignore = !ignore;
                        break;
                }
                if (quote)
                    sb.Append(ch);
                else
                {
                    switch (ch)
                    {
                        case '{':
                        case '[':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', ++offset * indentLength));
                            break;
                        case '}':
                        case ']':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', --offset * indentLength));
                            sb.Append(ch);
                            break;
                        case ',':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentLength));
                            break;
                        case ':':
                            sb.Append(ch);
                            sb.Append(' ');
                            break;
                        default:
                            if (ch != ' ') sb.Append(ch);
                            break;
                    }
                }
            }
            return sb.ToString().Trim();
        }
    }
}

