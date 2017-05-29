using System;
using System.Collections.Generic;
using System.Net.Http;


using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace test
{
	public partial class RestaurantsListPage : ContentPage
	{
		void Handle_SearchButtonPressed(object sender, System.EventArgs e)
		{
			listView.ItemsSource = new List<RestruantModel>();
			SearchRestaurantsAsync(SearchBarRestaurants.Text, (restaurantsResponse) =>
			{
				listView.ItemsSource = restaurantsResponse.restaurants;
			});
		}

		public RestaurantsListPage()
		{
			InitializeComponent();
			Title = "Search Restaurants";
		}

		async void SearchRestaurantsAsync(string searchText, Action<RestruantsResponse> callback)
		{
			HttpClient client = new HttpClient();
			string url = "https://developers.zomato.com/api/v2.1/search?q=" + searchText;
			var uri = new Uri(url);
			HttpResponseMessage response = null;
			client.MaxResponseContentBufferSize = 2560000;
			client.DefaultRequestHeaders.Add("user-key", "216b69fdc8edd8900e3721e83c543591");
			response = await client.GetAsync(uri);
			var contentBody = await response.Content.ReadAsStringAsync();

			//Parse the response
			RestruantsResponse restaurantResponse = JsonConvert.DeserializeObject<RestruantsResponse>(contentBody);
			List<RestruantModel> restModels = new List<RestruantModel>();
			Newtonsoft.Json.Linq.JObject obj = Newtonsoft.Json.Linq.JObject.Parse(contentBody);
			IList<JToken> results = obj["restaurants"].Children().ToList();
			foreach (JToken result in results)
			{
				RestruantModel restModel = result.ToObject<RestruantModel>();
				restModel.restaurant.address = result["restaurant"]["location"]["address"].ToString();
				restModels.Add(restModel);
			}
			restaurantResponse.restaurants = restModels;
			callback(restaurantResponse);
		}
	}
}
