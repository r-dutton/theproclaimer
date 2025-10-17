[web] DELETE /api/accounting/sourcedata/sources/{id:Guid}  (Cirrus.Api.Controllers.Accounting.SourceData.SourcesController.Delete)  [L150–L156] status=200 [auth=user]
  └─ calls SourceRepository (methods: Remove,WriteQuery) [L154]
  └─ delete Source [L154]
  └─ write Source [L153]
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ Source writes=2 reads=0

