[web] GET /api/binder-column-template-sets  (Workpapers.Next.API.Controllers.Templates.BinderColumnTemplateSetsController.GetAll)  [L27–L33]
  └─ sends_request GetBinderColumnTemplateSets [L32]
  └─ sends_request GetBinderColumnTemplateSetsLight [L31]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.GetBinderColumnTemplateSetsHandler.Handle [L17–L46]
      └─ uses_service IControlledRepository<BinderColumnTemplateSet>
        └─ method ReadQuery [L40]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L43]

