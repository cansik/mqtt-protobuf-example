#!/bin/zsh

# generates models from proto file with protogen
# installation: https://www.nuget.org/packages/protobuf-net.Protogen 
protogen --csharp_out=SimnetLib/Model --package=Simnet.Protocol Simnet.proto