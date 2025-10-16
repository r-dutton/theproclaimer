[web] PUT /api/binder-status-requirements/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderStatusRequirementsController.Update)  [L52–L62] status=200
  └─ calls BinderStatusRequirementsRepository.WriteQuery [L55]
  └─ write BinderStatusRequirements [L55]
    └─ reads_from BinderStatusRequirements
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ BinderStatusRequirements writes=1 reads=0

