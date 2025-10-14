[web] GET /api/matter-templates/for-workpaper-record-template/{workpaperRecordTemplateId:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTemplatesController.GetAllByWorkpaperRecord)  [L48–L56] [auth=AuthorizationPolicies.Administrator]
  └─ calls MatterTemplateRepository.ReadQuery [L51]
  └─ queries MatterTemplate [L51]
    └─ reads_from MatterTemplates
  └─ uses_service IControlledRepository<MatterTemplate>
    └─ method ReadQuery [L51]

