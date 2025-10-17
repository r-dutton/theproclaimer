[web] POST /api/binders/{binderId:Guid}/worksheets/{id:guid}/commands  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.AddWorksheetCommands)  [L377–L388] status=201 [auth=AuthorizationPolicies.User]
  └─ calls WorksheetRepository.WriteQuery [L381]
  └─ write Worksheet [L381]
    └─ reads_from Worksheets
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Worksheet writes=1 reads=0

