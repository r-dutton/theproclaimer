[web] POST /api/hmrc/vat/package/{vrn}  (Workpapers.Next.API.Controllers.Workpapers.HmrcController.GetVatPackage)  [L37–L45] [auth=AuthorizationPolicies.User]
  └─ sends_request GetVatPackageQuery [L42]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Hmrc.Api.Queries.Vat.GetVatPackageQueryHandler.Handle [L11–L39]
      └─ uses_client HmrcClient [L24]
      └─ uses_service HmrcClient
        └─ method GetVatLiabilities [L24]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L26]
  └─ returns VatPackageDto [L42]

