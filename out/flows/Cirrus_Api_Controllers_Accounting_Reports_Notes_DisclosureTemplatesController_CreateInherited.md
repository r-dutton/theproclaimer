[web] POST /api/accounting/reports/notes/disclosure-templates/inherit  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.CreateInherited)  [L153–L163] [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository.Add [L161]
  └─ calls DisclosureTemplateRepository.ReadQuery [L156]
  └─ queries DisclosureTemplate [L156]
    └─ reads_from DisclosureTemplates
  └─ writes_to DisclosureTemplate [L161]
    └─ reads_from DisclosureTemplates
  └─ uses_service IControlledRepository<DisclosureTemplate>
    └─ method ReadQuery [L156]

