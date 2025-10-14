[web] PUT /api/binder-roll-over/  (Workpapers.Next.API.Controllers.Workpapers.BinderRollOverController.RollOver)  [L76–L89] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.WriteQuery [L79]
  └─ writes_to Binder [L79]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method WriteQuery [L79]
  └─ sends_request RollOverBinderCommand [L86]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.Binders.RollOverBinderCommandHandler.Handle [L57–L894]
      └─ maps_to BinderSectionCreateModel [L251]
      └─ maps_to RollOverHyperlinkModel [L353]
      └─ maps_to RollOverMatterModel [L349]
      └─ maps_to RollOverRecordModel [L368]
      └─ maps_to RollOverRecordModel [L345]
      └─ uses_service IControlledRepository<ArchivedWorkpaperRecordTemplateMapping>
        └─ method ReadQuery [L320]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L251]
      └─ uses_service IControlledRepository<Matter>
        └─ method ReadQuery [L157]
      └─ uses_service IControlledRepository<MatterStatus> (AddScoped)
        └─ method ReadQuery [L165]
      └─ uses_service IControlledRepository<RollOverHyperlink>
        └─ method WriteQuery [L237]
      └─ uses_service IControlledRepository<RollOverMatter>
        └─ method WriteQuery [L233]
      └─ uses_service IControlledRepository<RollOverRecord>
        └─ method WriteQuery [L224]
      └─ uses_service IControlledRepository<RollOverWorksheet>
        └─ method WriteQuery [L229]
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method ReadQuery [L298]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L497]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L254]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L112]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L146]
      └─ uses_service UnitOfWork
        └─ method Table [L387]
      └─ uses_service UserService
        └─ method GetUserId [L179]
  └─ sends_request CanIAccessBinderQuery [L85]
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

