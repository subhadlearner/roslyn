﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Extensions;
using Microsoft.CodeAnalysis.CSharp.Extensions.ContextQuery;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Shared.Extensions;

namespace Microsoft.CodeAnalysis.CSharp.Completion.KeywordRecommenders
{
    internal class AssemblyKeywordRecommender : AbstractSyntacticSingleKeywordRecommender
    {
        public AssemblyKeywordRecommender()
            : base(SyntaxKind.AssemblyKeyword)
        {
        }

        protected override bool IsValidContext(int position, CSharpSyntaxContext context, CancellationToken cancellationToken)
        {
            if (context.TargetToken.GetNextToken().Kind() == SyntaxKind.NamespaceKeyword || context.IsTypeAttributeContext(cancellationToken) )
            {
                var token = context.LeftToken;
                var type = token.GetAncestor<MemberDeclarationSyntax>();

                return type == null || type.IsParentKind(SyntaxKind.CompilationUnit);
            }

            return false;
        }
    }
}
