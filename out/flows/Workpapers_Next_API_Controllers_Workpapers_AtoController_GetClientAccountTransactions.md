[web] GET /api/ato/client-account-transactions  (Workpapers.Next.API.Controllers.Workpapers.AtoController.GetClientAccountTransactions)  [L50–L60] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetClientAccountTransactionsQuery [L56]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetClientAccountTransactionsQueryHandler.Handle [L36–L77]
      └─ uses_client DataGetClient [L64]
        └─ calls GetTransactionDetail (GET /api/gov-link/client-accounts/detail?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, method=GetClientAccountDetailsAsync, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L250]
          └─ target_service DataGet.Api
            └─ [web] GET /api/gov-link/client-accounts/detail  (DataGet.Api.Controllers.GovLink.ClientAccountController.GetTransactionDetail)  [L22–L31] status=200 [auth=Authentication.MachineToMachinePolicy]
      └─ uses_service DataGetClient
        └─ method GetClientAccountDetailsAsync [L64]
          └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetClientAccountDetailsAsync [L32-L506]
            └─ calls GetTransactionDetail [L250]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L55]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

