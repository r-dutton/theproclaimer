[web] POST /api/hmrc/vat/package/{vrn}  (Workpapers.Next.API.Controllers.Workpapers.HmrcController.GetVatPackage)  [L37–L45] status=201 [auth=AuthorizationPolicies.User]
  └─ sends_request GetVatPackageQuery -> GetVatPackageQueryHandler [L42]
    └─ handled_by DataGet.Integrations.Hmrc.Api.Queries.Vat.GetVatPackageQueryHandler.Handle [L11–L39]
      └─ uses_client HmrcClient [L24]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L26]
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service HmrcClient
        └─ method GetVatLiabilities [L24]
          └─ implementation DataGet.Integrations.Hmrc.Api.Services.HmrcClient.GetVatLiabilities [L35-L298]
  └─ returns VatPackageDto [L42]
  └─ impact_summary
    └─ clients 1
      └─ HmrcClient
    └─ requests 1
      └─ GetVatPackageQuery
    └─ handlers 1
      └─ GetVatPackageQueryHandler
    └─ mappings 1
      └─ VatPackageDto

