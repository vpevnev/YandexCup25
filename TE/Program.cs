using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

// Решение на 23 балла.

namespace TE
{
    class Program
    {
        static async Task Main()
        {
            var map = new ResultContainer();

            var rawJson = Console.ReadLine();

            map = JsonSerializer.Deserialize<ResultContainer>(rawJson);

            Console.WriteLine("OK");

            var isEnded = false;

            while (true)
            {
                var key = Console.ReadLine();

                switch (key)
                {
                    case "SHUTDOWN":
                        Console.WriteLine("OK");
                        isEnded = true;
                        break;
                    case "NEXT":
                        try
                        {
                            var localResult = await Process(map.result);
                            Console.WriteLine(localResult);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                }

                if (isEnded)
                {
                    break;
                }
            }
        }

        public async static Task<int> Process(OperationNode node, int value = 0)
        {
            switch (node.operation)
            {
                case "&":
                    value = ~0;
                    foreach (var child in node.children)
                    {
                        var localValue = await Process(child, value);
                        value &= localValue;
                    }
                    break;
                case "|":
                    value = 0;
                    foreach (var child in node.children)
                    {
                        var localValue = await Process(child, value);
                        value |= localValue;
                    }
                    break;
                case "call":
                    value = await Get(node.backend);
                    break;
            }

            return value;
        }

        public async static Task<int> Get(string address)
        {
            //address = "http://localhost:8282/";

            using var httpClient = new HttpClient();

            string value = null;

            var apiClient = new ApiClient(httpClient, address);
                                                                  
            value = await apiClient.GetValueAsync("code");

            return Convert.ToInt32(value);
        }

        public class OperationNode
        {
            public string operation { get; set; }
            public List<OperationNode> children { get; set; }
            public string backend { get; set; }
        }

        public class ResultContainer
        {
            public OperationNode result { get; set; }
        }

        public class ApiClient
        {
            private readonly HttpClient _client;
            private readonly string _path;

            public ApiClient(HttpClient client, string path)
            {
                // Передаем клиенту базовую ссылку на API
                _client = client;
                _path = path;
                _client.BaseAddress = new Uri(_path);
            }

            /// <summary>
            /// Метод для получения значения по уникальному ключу.
            /// Соответствует спецификации OpenAPI v3.0.2 для метода GET "/{key}"
            /// </summary>
            /// <param name="key">Уникальный ключ</param>
            /// <returns>Значение по указанному ключу или сообщение об ошибке.</returns>
            public async Task<string> GetValueAsync(string key)
            {
                using CancellationTokenSource source = new CancellationTokenSource(TimeSpan.FromSeconds(1));

                try
                {
                    var response = await _client.GetAsync($"{key}", source.Token);

                    return await response.Content.ReadAsStringAsync();
                }
                catch (TaskCanceledException)
                {
                    throw new Exception("ERROR");
                }
            }
        }
    }
}
