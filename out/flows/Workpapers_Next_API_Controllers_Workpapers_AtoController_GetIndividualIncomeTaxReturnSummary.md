[web] GET /api/ato/individual-income-tax-returns/summary  (Workpapers.Next.API.Controllers.Workpapers.AtoController.GetIndividualIncomeTaxReturnSummary)  [L82–L88] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetIndividualIncomeTaxReturnSummaryQuery [L85]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetIndividualIncomeTaxReturnSummaryQueryHandler.Handle [L32–L72]
      └─ uses_client DataGetClient [L59]
        └─ calls GetReportSummary (GET /api/gov-link/individual-income-tax-returns/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, method=GetIndividualIncomeTaxReturnSummaryAsync, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L307]
          └─ target_service DataGet.Api
            └─ [web] GET /api/gov-link/individual-income-tax-returns/summary  (DataGet.Api.Controllers.GovLink.IndividualIncomeTaxReturnController.GetReportSummary)  [L34–L42] status=200 [auth=Authentication.MachineToMachinePolicy]
      └─ uses_service DataGetClient
        └─ method GetIndividualIncomeTaxReturnSummaryAsync [L59]
          └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetIndividualIncomeTaxReturnSummaryAsync [L32-L506]
            └─ calls GetReportSummary [L307]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L51]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

