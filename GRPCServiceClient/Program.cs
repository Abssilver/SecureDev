// See https://aka.ms/new-console-template for more information

using Grpc.Net.Client;
using GRPCServices.Proto;

Console.WriteLine("Press any key to run client ...");
Console.ReadLine();

using GrpcChannel channel = GrpcChannel.ForAddress("testAddress");
ClientService.ClientServiceClient client = new ClientService.ClientServiceClient(channel);
var response = client.Create(new CreateClientRequest
{
    FirstName = "TestName",
    Surname = "TestSurname",
    Patronymic = "TestPatronymic"
});

Console.WriteLine($"ClientId: {response.ClientId}");