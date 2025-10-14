[web] GET /api/binder-column-template-sets/for-binder-type/{binderTypeId:guid}  (Workpapers.Next.API.Controllers.Templates.BinderColumnTemplateSetsController.GetForBinderType)  [L41–L45]
  └─ sends_request GetBinderColumnTemplateSetForBinderType [L44]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.GetBinderColumnTemplateSetForBinderTypeHandler.Handle [L24–L45]
      └─ maps_to BinderColumnTemplateSetDto [L37]
        └─ automapper.registration WorkpapersMappingProfile (BinderColumnTemplateSet->BinderColumnTemplateSetDto) [L368]
      └─ uses_service IControlledRepository<BinderColumnTemplateSet>
        └─ method ReadQuery [L37]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L40]

