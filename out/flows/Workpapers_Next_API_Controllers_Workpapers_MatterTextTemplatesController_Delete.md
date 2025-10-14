[web] DELETE /api/matter-text-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.Delete)  [L167–L179] [auth=AuthorizationPolicies.Administrator]
  └─ calls MatterTextTemplateRepository.Remove [L176]
  └─ calls MatterTextTemplateRepository.WriteQuery [L171]
  └─ writes_to MatterTextTemplate [L176]
    └─ reads_from MatterTextTemplates
  └─ writes_to MatterTextTemplate [L171]
    └─ reads_from MatterTextTemplates
  └─ uses_service IControlledRepository<MatterTextTemplate>
    └─ method WriteQuery [L171]
  └─ uses_service UserService
    └─ method IsSuperUser [L173]

