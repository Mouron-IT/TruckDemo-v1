using Azure;
using Azure.AI.OpenAI;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Models;

using MediatR;

using System.Text.Json;

using TruckDemo_v1.Application.DTO.Result;

namespace TruckDemo_v1.Application.UseCases.Assistants.Question
{
    public class QuestionRequestHandler : IRequestHandler<QuestionRequest, Result<QuestionResponse>>
    {

        public QuestionRequestHandler()
        {
        }

        public async Task<Result<QuestionResponse>> Handle(QuestionRequest request, CancellationToken cancellationToken)
        {
            Uri oaiEndpoint = new("https://cog-k7vjdn3a6kyqe.openai.azure.com/");
            string oaiKey = "e86e5abca96f45bba84f6720daf1863d";

            AzureKeyCredential credentials = new(oaiKey);

            OpenAIClient openAIClient = new(oaiEndpoint, credentials);



            var searchCredential = new AzureKeyCredential("0tXtY417VP9B5lUu2N6lUk2UUFSigtfFvFpmxATW92AzSeCbgbAg");
            var indexClient = new SearchIndexClient(new Uri("https://gptkb-k7vjdn3a6kyqe.search.windows.net"), searchCredential);
            var searchClient = indexClient.GetSearchClient("gptkbindex");




            EmbeddingsOptions embeddingOptions = new("embedding", new string[] { request.Question });

            var resultvector = openAIClient.GetEmbeddings(embeddingOptions);

            var queryEmbeddings = resultvector.Value.Data[0].Embedding;

            // Perform the vector similarity search  
            var searchOptions = new SearchOptions
            {
                VectorQueries = { new RawVectorQuery() { Vector = queryEmbeddings.ToArray(), KNearestNeighborsCount = 3, Fields = { "embedding" } } },
                Size = 3,
                Select = {  "content" },
            };

            SearchResults<SearchDocument> response = await searchClient.SearchAsync<SearchDocument>(null, searchOptions);

            int count = 0;

            List<string> answers = new();
            await foreach (SearchResult<SearchDocument> result in response.GetResultsAsync())
            {
                count++;

                answers.Add(result.Document["content"].ToString());

                //Console.WriteLine($"Content: {result.Document["content"]}");

            }



            string promptTemplate = """
                El usuario se llama {Name}.
                El usuario es un novato introduciendose al manejo de tractores.
                EL usuario realizo la siguiente pregunta: {Question}

                Generame una respuesta corta, amigable y personalizada utilizando solamente lo que sirva del siguiente contenido
                
                {Content}
                """;

            string prompt = promptTemplate.Replace("{Name}", request.Name)
                .Replace("{Question}", request.Question)
                .Replace("{Content}", JsonSerializer.Serialize(answers));


            Response<ChatCompletions> responseWithoutStream = await openAIClient.GetChatCompletionsAsync(
                
                new ChatCompletionsOptions()
                {
                    DeploymentName = "chat",
                    Messages =
                                                    {
                                                        new ChatMessage(ChatRole.User, prompt),
                                                    },
                    Temperature = (float)1,
                    MaxTokens = 2041,
                    NucleusSamplingFactor = (float)0.95,
                    FrequencyPenalty = 0,
                    PresencePenalty = 0,
                });

                        ChatCompletions completions = responseWithoutStream.Value;

            return new QuestionResponse(completions.Choices.First().Message.Content);
        }
    }
}
