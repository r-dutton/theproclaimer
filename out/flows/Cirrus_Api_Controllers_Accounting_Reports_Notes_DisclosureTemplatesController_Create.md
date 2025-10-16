[web] POST /api/accounting/reports/notes/disclosure-templates/master/file  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.Create)  [L121–L147] status=201 [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository.Add [L145]
  └─ calls DisclosureTemplateRepository.WriteQuery [L137]
  └─ calls DisclosureTemplateRepository.ReadQuery [L127]
  └─ insert DisclosureTemplate [L145]
    └─ reads_from DisclosureTemplates
  └─ query DisclosureTemplate [L127]
    └─ reads_from DisclosureTemplates
  └─ write DisclosureTemplate [L137]
    └─ reads_from DisclosureTemplates
  └─ uses_service IControlledRepository<DisclosureTemplate>
    └─ method ReadQuery [L127]
      └─ ... (no implementation details available)

