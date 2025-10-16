[web] GET /api/binder-column-template-sets  (Workpapers.Next.API.Controllers.Templates.BinderColumnTemplateSetsController.GetAll)  [L27–L33] status=200
  └─ sends_request GetBinderColumnTemplateSets [L32]
  └─ sends_request GetBinderColumnTemplateSetsLight [L31]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.GetBinderColumnTemplateSetsHandler.Handle [L17–L46]
      └─ uses_service IControlledRepository<BinderColumnTemplateSet>
        └─ method ReadQuery [L40]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L43]
          └─ ... (no implementation details available)

