[web] PUT /api/accounting/reports/notes/disclosure-templates/{id:Guid}/reorder/file  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.Reorder)  [L76–L98] status=200 [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository.WriteQuery [L83]
  └─ calls DisclosureTemplateRepository.ReadQuery [L79]
  └─ query DisclosureTemplate [L79]
    └─ reads_from DisclosureTemplates
  └─ write DisclosureTemplate [L83]
    └─ reads_from DisclosureTemplates
  └─ uses_service IControlledRepository<DisclosureTemplate>
    └─ method ReadQuery [L79]
      └─ ... (no implementation details available)

