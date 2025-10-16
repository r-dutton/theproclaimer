[web] PUT /api/matter-text-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.Update)  [L153–L165] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatterTextTemplateRepository.WriteQuery [L157]
  └─ write MatterTextTemplate [L157]
    └─ reads_from MatterTextTemplates
  └─ uses_service IControlledRepository<MatterTextTemplate>
    └─ method WriteQuery [L157]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L159]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]

