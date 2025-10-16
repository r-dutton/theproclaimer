[web] GET /api/matter-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTemplatesController.Get)  [L58–L65] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to MatterTemplateDto [L61]
    └─ automapper.registration WorkpapersMappingProfile (MatterTemplate->MatterTemplateDto) [L790]
  └─ calls MatterTemplateRepository.ReadQuery [L61]
  └─ query MatterTemplate [L61]
    └─ reads_from MatterTemplates
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ MatterTemplate writes=0 reads=1
    └─ mappings 1
      └─ MatterTemplateDto

