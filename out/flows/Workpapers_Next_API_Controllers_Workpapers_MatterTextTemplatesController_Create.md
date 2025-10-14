[web] POST /api/matter-text-templates/  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.Create)  [L140–L151] [auth=AuthorizationPolicies.Administrator]
  └─ calls MatterTextTemplateRepository.Add [L148]
  └─ writes_to MatterTextTemplate [L148]
    └─ reads_from MatterTextTemplates
  └─ uses_service IControlledRepository<MatterTextTemplate>
    └─ method Add [L148]
  └─ uses_service UserService
    └─ method IsSuperUser [L144]

