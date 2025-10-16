[web] POST /api/hmrc/vat/package/{vrn}  (Workpapers.Next.API.Controllers.Workpapers.HmrcController.GetVatPackage)  [L37–L45] status=201 [auth=AuthorizationPolicies.User]
  └─ sends_request GetVatPackageQuery [L42]
    └─ handled_by DataGet.Integrations.Hmrc.Api.Queries.Vat.GetVatPackageQueryHandler.Handle [L11–L39]
      └─ uses_client HmrcClient [L24]
      └─ uses_service HmrcClient
        └─ method GetVatLiabilities [L24]
          └─ implementation DataGet.Integrations.Hmrc.Api.Services.HmrcClient.GetVatLiabilities [L35-L298]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L26]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
  └─ returns VatPackageDto [L42]

