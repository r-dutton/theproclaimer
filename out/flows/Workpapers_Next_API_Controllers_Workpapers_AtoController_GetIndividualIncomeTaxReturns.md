[web] GET /api/ato/individual-income-tax-returns  (Workpapers.Next.API.Controllers.Workpapers.AtoController.GetIndividualIncomeTaxReturns)  [L74–L80] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetIndividualIncomeTaxReturnQuery [L77]
    └─ handled_by GovLink.Application.Queries.IITR.GetIndividualIncomeTaxReturnQueryHandler.Handle [L89–L120]
      └─ uses_client IAtoClient [L103]
      └─ uses_service IAtoClient (AddScoped)
        └─ method RequestAsync [L103]
          └─ ... (no implementation details available)
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetIndividualIncomeTaxReturnQueryHandler.Handle [L33–L73]
      └─ uses_client DataGetClient [L60]
      └─ uses_service DataGetClient
        └─ method GetIndividualIncomeTaxReturnAsync [L60]
          └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetIndividualIncomeTaxReturnAsync [L32-L506]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L52]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
  └─ returns IndividualIncomeTaxReturnDto [L77]

