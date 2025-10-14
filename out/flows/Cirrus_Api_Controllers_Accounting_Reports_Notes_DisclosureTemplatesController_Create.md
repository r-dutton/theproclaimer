[web] POST /api/accounting/reports/notes/disclosure-templates/master/file  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.Create)  [L121–L147] [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository.Add [L145]
  └─ calls DisclosureTemplateRepository.WriteQuery [L137]
  └─ calls DisclosureTemplateRepository.ReadQuery [L127]
  └─ queries DisclosureTemplate [L127]
    └─ reads_from DisclosureTemplates
  └─ writes_to DisclosureTemplate [L145]
    └─ reads_from DisclosureTemplates
  └─ writes_to DisclosureTemplate [L137]
    └─ reads_from DisclosureTemplates
  └─ uses_service IControlledRepository<DisclosureTemplate>
    └─ method ReadQuery [L127]

