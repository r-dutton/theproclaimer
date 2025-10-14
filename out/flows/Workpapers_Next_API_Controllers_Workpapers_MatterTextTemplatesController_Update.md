[web] PUT /api/matter-text-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.Update)  [L153–L165] [auth=AuthorizationPolicies.Administrator]
  └─ calls MatterTextTemplateRepository.WriteQuery [L157]
  └─ writes_to MatterTextTemplate [L157]
    └─ reads_from MatterTextTemplates
  └─ uses_service IControlledRepository<MatterTextTemplate>
    └─ method WriteQuery [L157]
  └─ uses_service UserService
    └─ method IsSuperUser [L159]

