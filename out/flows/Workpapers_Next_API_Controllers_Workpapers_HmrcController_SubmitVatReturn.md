[web] POST /api/hmrc/vat/submit  (Workpapers.Next.API.Controllers.Workpapers.HmrcController.SubmitVatReturn)  [L68–L78] [auth=AuthorizationPolicies.User]
  └─ sends_request SubmitVatReturnCommand [L77]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Hmrc.Api.Commands.Vat.SubmitVatReturnCommandHandler.Handle [L14–L28]
      └─ uses_client HmrcClient [L25]
      └─ uses_service HmrcClient
        └─ method SubmitVatReturn [L25]
  └─ sends_request CanIAccessBinderQuery [L72]
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

