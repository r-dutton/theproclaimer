[web] POST /api/accounting/reports/notes/disclosure-templates/inherit  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.CreateInherited)  [L153–L163] status=201 [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository.Add [L161]
  └─ calls DisclosureTemplateRepository.ReadQuery [L156]
  └─ insert DisclosureTemplate [L161]
    └─ reads_from DisclosureTemplates
  └─ query DisclosureTemplate [L156]
    └─ reads_from DisclosureTemplates
  └─ uses_service IControlledRepository<DisclosureTemplate>
    └─ method ReadQuery [L156]
      └─ ... (no implementation details available)

