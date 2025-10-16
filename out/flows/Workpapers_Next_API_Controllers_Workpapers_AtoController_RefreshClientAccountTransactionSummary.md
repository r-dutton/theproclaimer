[web] GET /api/ato/gov-link/client-account-transactions/summary  (Workpapers.Next.API.Controllers.Workpapers.AtoController.RefreshClientAccountTransactionSummary)  [L149–L160] status=200 [auth=AuthorizationPolicies.User,AuthorizationPolicies.GovLink]
  └─ sends_request GetClientAccountTransactionSummaryQuery [L156]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetClientAccountTransactionSummaryQueryHandler.Handle [L35–L70]
      └─ uses_client DataGetClient [L64]
        └─ calls GetTransactionSummary (GET /api/gov-link/client-accounts/summary?clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, method=GetClientAccountSummaryAsync, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientAccountType={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L269]
          └─ target_service DataGet.Api
            └─ [web] GET /api/gov-link/client-accounts/summary  (DataGet.Api.Controllers.GovLink.ClientAccountController.GetTransactionSummary)  [L33–L42] status=200 [auth=Authentication.MachineToMachinePolicy]
      └─ uses_service DataGetClient
        └─ method GetClientAccountSummaryAsync [L64]
          └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetClientAccountSummaryAsync [L32-L506]
            └─ calls GetTransactionSummary [L269]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L54]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

