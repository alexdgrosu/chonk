![CHONK logo](./docs/logo.png)

CHONK 🦔: Can't Help Over-engineering New Krap
==========================================

This is a pet project where I want to see how far I can hilariously over-engineer a common task but still produce something that is  architecturally sane. The idea came one late evening when my better half wanted to a simple yes/no answer to whether or not Starbucks is still open.

Why build a simple script that crawls [starbucks.com](https://www.starbucks.com) for an answer that _I_ have to run when I can over-engineer the krap out of a system to do it _for me_?

I took to our amazing pet hedgehog for inspiration. The same way she can run incredibly fast & smooth despite her physics-defying round shape and the 30-odd worms she ate moments prior, _I too_ can build a system that performs reasonably well _in spite of_ being bloated with all kinds of architectural patterns and components.

## The Gist of It
The gist of 🦔 is that 
- it is deployed in a fully containerized environment
- it enables triggering containerized workloads on the same environment
- it provides some sort of introspection into the each workload via simple real-time logging

![The Gist of Chonk](./docs/the-gist-of-chonk.png)
> _e.g._  
Triggering the _"Is Starbucks open?"_ workload could deploy a container which scrapes starbucks.com for an answer.  
Upon completion, the container reports back **Yes/No** (or possibly fails).

