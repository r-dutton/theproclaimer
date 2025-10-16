[web] POST /api/matter-text-templates/  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.Create)  [L140–L151] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatterTextTemplateRepository.Add [L148]
  └─ insert MatterTextTemplate [L148]
    └─ reads_from MatterTextTemplates
  └─ uses_service IControlledRepository<MatterTextTemplate>
    └─ method Add [L148]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L144]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]

