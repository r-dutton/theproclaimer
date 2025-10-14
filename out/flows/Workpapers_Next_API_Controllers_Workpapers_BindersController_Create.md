[web] POST /api/binders/  (Workpapers.Next.API.Controllers.Workpapers.BindersController.Create)  [L189–L242] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.Add [L221]
  └─ writes_to Binder [L221]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method Add [L221]
  └─ uses_service UserService
    └─ method GetUser [L229]
  └─ sends_request CreateBinderDocumentCommand [L229]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Binders.CreateBinderDocumentCommandHandler.Handle [L32–L66]
      └─ uses_service DataverseService
        └─ method GetStandardQueryParams [L60]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L51]
  └─ sends_request CanBinderBeCreatedQuery [L215]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanBinderBeCreatedQueryHandler.Handle [L36–L151]
      └─ maps_to BinderTemplateDto [L77]
        └─ automapper.registration WorkpapersMappingProfile (BinderTemplate->BinderTemplateDto) [L726]
      └─ uses_service IControlledRepository<BinderTemplate>
        └─ method ReadQuery [L77]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L117]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L63]

