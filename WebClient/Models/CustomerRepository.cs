using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;

namespace WebClient.Models
{
    public class CustomerRepository
    {
        private string ApiUrl = "https://localhost:44350/Customers";
        private HttpClient _client;
        public CustomerRepository()
        {
            _client = new HttpClient();
        }
        public List<Customer> GetAllCustomer(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = _client.GetStringAsync(ApiUrl).Result;
            List<Customer> list = JsonConvert.DeserializeObject<List<Customer>>(result);
            return list;
        }

        public Customer GetCustomerById(int id , string token )
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = _client.GetStringAsync(ApiUrl + "/" + id).Result;
            Customer customer = JsonConvert.DeserializeObject<Customer>(result);
            return customer;
        }

        public void AddCustomer(Customer customer,string token )
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string jsoncontent = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
            var res = _client.PostAsync(ApiUrl, content).Result;
            
        }

        public void EditCustomer(Customer customer,string token )
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string jsoncontent = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
            var res = _client.PutAsync(ApiUrl + "/" + customer.CustomerId, content);
        }
        public void DeleteCustomer(int customerid,string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var res = _client.DeleteAsync(ApiUrl + "/" + customerid);
        }



    }
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
