[web] GET /api/ato/profile-compare  (Workpapers.Next.API.Controllers.Workpapers.AtoController.GetProfileComparison)  [L110–L116] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetIndividualIncomeTaxReturnProfileCompareQuery [L113]
    └─ handled_by GovLink.Application.Queries.IITR.GetIndividualIncomeTaxReturnProfileCompareQueryHandler.Handle [L116–L134]
      └─ uses_client IAtoClient [L130]
      └─ uses_service IAtoClient (AddScoped)
        └─ method RequestAsync [L130]
          └─ ... (no implementation details available)
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetIndividualIncomeTaxReturnProfileCompareQueryHandler.Handle [L33–L73]
      └─ uses_client DataGetClient [L60]
        └─ calls GetProfileComparison (GET /api/gov-link/individual-income-tax-returns/profile-compare?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, method=GetProfileCompareAsync, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L325]
          └─ target_service DataGet.Api
            └─ [web] GET /api/gov-link/individual-income-tax-returns/profile-compare  (DataGet.Api.Controllers.GovLink.IndividualIncomeTaxReturnController.GetProfileComparison)  [L44–L53] status=200 [auth=Authentication.MachineToMachinePolicy]
      └─ uses_service DataGetClient
        └─ method GetProfileCompareAsync [L60]
          └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetProfileCompareAsync [L32-L506]
            └─ calls GetProfileComparison [L325]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L52]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
  └─ returns ProfileCompareDto [L113]

