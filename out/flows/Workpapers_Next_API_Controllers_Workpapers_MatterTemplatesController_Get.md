[web] GET /api/matter-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTemplatesController.Get)  [L58–L65] [auth=AuthorizationPolicies.Administrator]
  └─ maps_to MatterTemplateDto [L61]
    └─ automapper.registration WorkpapersMappingProfile (MatterTemplate->MatterTemplateDto) [L790]
  └─ calls MatterTemplateRepository.ReadQuery [L61]
  └─ queries MatterTemplate [L61]
    └─ reads_from MatterTemplates
  └─ uses_service IControlledRepository<MatterTemplate>
    └─ method ReadQuery [L61]

