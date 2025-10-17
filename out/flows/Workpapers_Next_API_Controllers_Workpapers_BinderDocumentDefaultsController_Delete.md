[web] DELETE /api/binder-document-defaults/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderDocumentDefaultsController.Delete)  [L89–L99] status=200
  └─ calls BinderDocumentDefaultsRepository (methods: Remove,WriteQuery) [L96]
  └─ delete BinderDocumentDefaults [L96]
    └─ reads_from BinderDocumentDefaults
  └─ write BinderDocumentDefaults [L92]
    └─ reads_from BinderDocumentDefaults
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ BinderDocumentDefaults writes=2 reads=0

