# Autotagging using Pulumi, .NET Core and Automation API

## Pre-requisites

- .NET Core (at least v6)
- Pulumi CLI installed
- An AWS account with credentials already set up 
- AWS CLI

## Instructions:

1. Clone this repo
1. Run `dotnet build && dotnet run`

You'll see the bucket name in the console output

To confirm that this has worked, run the following:

`aws s3api get-bucket-tagging --bucket {bucket name}`

To destroy the resources:

`dotnet run destroy`

To delete the stack:

`dotnet run deletestack`
