[web] PUT /api/subscriptions/{id:int}  (Workpapers.Next.API.Controllers.SubscriptionsController.PutSubscription)  [L74–L85] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ uses_service UnitOfWork
    └─ method Table [L77]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

