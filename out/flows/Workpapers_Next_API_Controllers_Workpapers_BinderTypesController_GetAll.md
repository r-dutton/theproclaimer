[web] GET /api/binder-types/  (Workpapers.Next.API.Controllers.Workpapers.BinderTypesController.GetAll)  [L48–L54] status=200
  └─ sends_request GetValidBinderTypesQuery -> GetValidBinderTypesQueryHandler [L51]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetValidBinderTypesQueryHandler.Handle [L31–L77]
      └─ maps_to BinderTypeLightDto [L69]
        └─ automapper.registration WorkpapersMappingProfile (BinderType->BinderTypeLightDto) [L762]
      └─ uses_service IControlledRepository<BinderType> (Scoped (inferred))
        └─ method ReadQuery [L69]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.BinderTypeRepository.ReadQuery
      └─ uses_service IControlledRepository<BinderTemplate> (Scoped (inferred))
        └─ method ReadQuery [L57]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.BinderTemplateRepository.ReadQuery
      └─ uses_service IControlledRepository<ExcludedBinderTemplate> (Scoped (inferred))
        └─ method ReadQuery [L55]
          └─ implementation Workpapers.Next.Data.Repository.Templates.ExcludedBinderTemplateRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetValidBinderTypesQuery
    └─ handlers 1
      └─ GetValidBinderTypesQueryHandler
    └─ mappings 1
      └─ BinderTypeLightDto

