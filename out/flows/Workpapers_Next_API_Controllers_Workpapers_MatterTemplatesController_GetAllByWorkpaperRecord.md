[web] GET /api/matter-templates/for-workpaper-record-template/{workpaperRecordTemplateId:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatterTemplatesController.GetAllByWorkpaperRecord)  [L48–L56] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatterTemplateRepository.ReadQuery [L51]
  └─ query MatterTemplate [L51]
    └─ reads_from MatterTemplates
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ MatterTemplate writes=0 reads=1

