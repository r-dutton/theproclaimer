[web] GET /api/binder-column-template-sets/{id:guid}  (Workpapers.Next.API.Controllers.Templates.BinderColumnTemplateSetsController.Get)  [L35–L39] status=200
  └─ sends_request GetBinderColumnTemplateSet [L38]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.GetBinderColumnTemplateSetHandler.Handle [L22–L40]
      └─ maps_to BinderColumnTemplateSetDto [L35]
        └─ automapper.registration WorkpapersMappingProfile (BinderColumnTemplateSet->BinderColumnTemplateSetDto) [L368]
      └─ uses_service IControlledRepository<BinderColumnTemplateSet>
        └─ method ReadQuery [L35]
          └─ ... (no implementation details available)

