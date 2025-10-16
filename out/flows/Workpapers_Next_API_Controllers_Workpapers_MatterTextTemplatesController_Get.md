[web] GET /api/matter-text-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.Get)  [L130–L138] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to MatterTextTemplateDto [L134]
    └─ automapper.registration WorkpapersMappingProfile (MatterTextTemplate->MatterTextTemplateDto) [L800]
  └─ calls MatterTextTemplateRepository.ReadQuery [L134]
  └─ query MatterTextTemplate [L134]
    └─ reads_from MatterTextTemplates
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ MatterTextTemplate writes=0 reads=1
    └─ mappings 1
      └─ MatterTextTemplateDto

