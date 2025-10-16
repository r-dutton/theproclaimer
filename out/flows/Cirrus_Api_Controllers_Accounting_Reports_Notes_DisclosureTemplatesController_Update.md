[web] PUT /api/accounting/reports/notes/disclosure-templates/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.Update)  [L168–L175] status=200 [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository.WriteQuery [L171]
  └─ write DisclosureTemplate [L171]
    └─ reads_from DisclosureTemplates
  └─ uses_service IControlledRepository<DisclosureTemplate>
    └─ method WriteQuery [L171]
      └─ ... (no implementation details available)

