[web] PUT /api/accounting/reports/notes/disclosure-templates/{id:Guid}/reorder/file  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.Reorder)  [L76–L98] [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository.WriteQuery [L83]
  └─ calls DisclosureTemplateRepository.ReadQuery [L79]
  └─ queries DisclosureTemplate [L79]
    └─ reads_from DisclosureTemplates
  └─ writes_to DisclosureTemplate [L83]
    └─ reads_from DisclosureTemplates
  └─ uses_service IControlledRepository<DisclosureTemplate>
    └─ method ReadQuery [L79]

