# UDiff

A UASSET diff/merge helper tool for git

I created this because I was having a hard time rebasing my blueprint files and Epic provides a nice tool to diff blueprint files in the editor but there is no support for git in the editor. The use case I am programming for is when you are mid rebase and have to merge two blueprint files.

## Installing

1) `git clone https://github.com/ruba1987/UDiff.git`
2) Add C:\the\repo\dir\bin to your PATH

## Commands {WIP}

git udiff - This command creates new files for you to diff in the editor
git umerge - This command will tell UDiff that you are all done fixing the files and you are ready to finalize them

## Usage {WIP}
So you are mid rebase and git is telling you that you have a conflict.

From the root of your project run: `git udiff`

This will create some new files that you can then diff in the editor. Make the changes you want in the editor using the built in diff tools and then save them.

From the root of your project run: `git umerge`

This will ask you some questions about what you just did and save some new files over the original(s). It will then cleanup after itself. At this point, if you would like, you can validate that the original blueprints have been updated by viewing them in the editor.

Once you are happy simply continue with the rebase as normal.

## Updating

A simple `git pull` will update you to the latest.
