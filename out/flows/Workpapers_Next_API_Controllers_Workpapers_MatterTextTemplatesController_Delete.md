[web] DELETE /api/matter-text-templates/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.Delete)  [L167–L179] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatterTextTemplateRepository.Remove [L176]
  └─ calls MatterTextTemplateRepository.WriteQuery [L171]
  └─ delete MatterTextTemplate [L176]
    └─ reads_from MatterTextTemplates
  └─ write MatterTextTemplate [L171]
    └─ reads_from MatterTextTemplates
  └─ uses_service IControlledRepository<MatterTextTemplate>
    └─ method WriteQuery [L171]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L173]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]

