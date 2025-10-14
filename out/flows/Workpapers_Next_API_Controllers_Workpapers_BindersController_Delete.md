[web] DELETE /api/binders/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BindersController.Delete)  [L475–L511] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.Remove [L502]
  └─ calls BinderRepository.WriteQuery [L481]
  └─ writes_to Binder [L502]
    └─ reads_from Binders
  └─ writes_to Binder [L481]
    └─ reads_from Binders
  └─ uses_service DataverseService
    └─ method GetStandardQueryParams [L490]
  └─ uses_service IControlledRepository<Binder>
    └─ method WriteQuery [L481]
  └─ uses_service UserService
    └─ method GetUser [L491]
  └─ sends_request CreateDataverseAuditHistoryCommand [L504]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Firm.CreateDataverseAuditHistoryQueryHandler.Handle [L37–L84]
      └─ uses_service DataverseService
        └─ method Post [L75]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L52]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUser [L53]
  └─ sends_request CanIAccessBinderQuery [L483]
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

