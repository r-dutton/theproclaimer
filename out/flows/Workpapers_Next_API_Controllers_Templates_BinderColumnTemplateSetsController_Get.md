[web] GET /api/binder-column-template-sets/{id:guid}  (Workpapers.Next.API.Controllers.Templates.BinderColumnTemplateSetsController.Get)  [L35–L39] status=200
  └─ sends_request GetBinderColumnTemplateSet -> GetBinderColumnTemplateSetHandler [L38]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.GetBinderColumnTemplateSetHandler.Handle [L22–L40]
      └─ maps_to BinderColumnTemplateSetDto [L35]
        └─ automapper.registration WorkpapersMappingProfile (BinderColumnTemplateSet->BinderColumnTemplateSetDto) [L368]
      └─ uses_service IControlledRepository<BinderColumnTemplateSet> (Scoped (inferred))
        └─ method ReadQuery [L35]
          └─ implementation Workpapers.Next.Data.Repository.Templates.BinderColumnTemplateSetRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetBinderColumnTemplateSet
    └─ handlers 1
      └─ GetBinderColumnTemplateSetHandler
    └─ mappings 1
      └─ BinderColumnTemplateSetDto

