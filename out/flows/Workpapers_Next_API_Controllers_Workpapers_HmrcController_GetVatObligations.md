[web] POST /api/hmrc/vat/obligations  (Workpapers.Next.API.Controllers.Workpapers.HmrcController.GetVatObligations)  [L47–L66] [auth=AuthorizationPolicies.User]
  └─ sends_request GetVatObligationsQuery [L65]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Hmrc.Api.Queries.Vat.GetVatObligationsQueryHandler.Handle [L22–L35]
      └─ uses_client HmrcClient [L33]
      └─ uses_service HmrcClient
        └─ method GetVatObligations [L33]
  └─ sends_request CanIAccessBinderQuery [L52]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L101]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
      └─ uses_service UserService
        └─ method GetUserId [L91]
      └─ uses_cache IDistributedCache [L121]
        └─ method SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache [L109]
        └─ method DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache [L92]
        └─ method CreateAccessKey [write] [L92]

